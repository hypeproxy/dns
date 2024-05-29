using DNS.Server;

var masterFile = new MasterFile();
var server = new DnsServer(masterFile, "192.168.8.1");

// Resolve these domain to localhost
masterFile.AddIPAddressResourceRecord("google.com", "127.0.0.1");
masterFile.AddIPAddressResourceRecord("github.com", "127.0.0.1");

// Log every request
server.Requested += (sender, e) =>
{
	Console.WriteLine($"{e.Remote.Address}:{e.Remote.Port}{e.Remote.AddressFamily.ToString()}");
	// Console.WriteLine(e.Request);
};
server.Responded += (sender, e) =>
{
	// Console.WriteLine("{0} => {1}", e.Request, e.Response);
};
server.Errored += (sender, e) => Console.WriteLine(e.Exception.Message);
server.Listening += (sender, eventArgs) =>
{
	Console.Write("Listening...");
};

// Start the server (by default it listens on port 53)
await server.Listen();