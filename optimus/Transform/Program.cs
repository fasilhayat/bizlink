using Microsoft.Extensions.Configuration;
using Transform;
using Transform.Entities;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Read folders from the configuration
var sourceFolder = config["Folders:SourceFolder"];
var targetFolder = config["Folders:TargetFolder"];

// Read schema to XSLT mappings
var schemaXsltMappings = config.GetSection("SchemaXsltMappings")
    .Get<SchemaMapping[]>();

// Create FolderMonitor and pass config parameters
FolderMonitor monitor = new FolderMonitor(sourceFolder, targetFolder, schemaXsltMappings);
monitor.StartMonitoring();

Console.WriteLine("Press any key to exit...");
Console.ReadKey();
