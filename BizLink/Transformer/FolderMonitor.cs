namespace Transformer;

using System.Xml;
using Entities;

public class FolderMonitor
{
    private readonly string? _sourceFolder;
    private readonly string? _targetFolder;
    private readonly SchemaMapping[]? _schemaXsltMappings;

    public FolderMonitor(string? sourceFolder, string targetFolder, SchemaMapping[] schemaXsltMappings)
    {
        this._sourceFolder = sourceFolder;
        this._targetFolder = targetFolder;
        this._schemaXsltMappings = schemaXsltMappings;
    }

    public void StartMonitoring()
    {
        FileSystemWatcher watcher = new FileSystemWatcher(_sourceFolder)
        {
            Filter = "*.xml", // Or "*.json" depending on the files you need to process
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite
        };

        watcher.Created += OnFileCreated;
        watcher.EnableRaisingEvents = true;

        Console.WriteLine("Monitoring folder: " + _sourceFolder);
    }

    private void OnFileCreated(object sender, FileSystemEventArgs e)
    {
        if (e.ChangeType == WatcherChangeTypes.Created)
        {
            ProcessFile(e.FullPath);
        }
    }

    private void ProcessFile(string filePath)
    {
        // Process the XML file
        var xsltPath = GetXsltForFile(filePath);
        if (!string.IsNullOrEmpty(xsltPath))
        {
            TransformXml(filePath, xsltPath);
        }
    }

    private string GetXsltForFile(string xmlFilePath)
    {
        // Load XML schema of the file and find the correct XSLT from the configuration
        var schema = GetXmlSchema(xmlFilePath);  // Implement logic to extract schema from XML

        foreach (var mapping in _schemaXsltMappings)
        {
            if (schema == mapping.Schema)
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, mapping.Xslt);
            }
        }

        return null;
    }

    private string GetXmlSchema(string xmlFilePath)
    {
        // Logic to extract XML schema from the XML file
        // You can use XmlReader and read the schema or use a third-party library
        return "schema1.xsd"; // This should dynamically fetch based on the XML
    }

    private void TransformXml(string xmlFilePath, string xsltPath)
    {
        if (xsltPath == null)
        {
            Console.WriteLine("No XSLT found for XML file: " + xmlFilePath);
            return;
        }

        string targetFilePath = Path.Combine(_targetFolder, Path.GetFileName(xmlFilePath));
        XslTransformXml(xmlFilePath, xsltPath, targetFilePath);
    }

    private void XslTransformXml(string xmlFilePath, string xsltFilePath, string outputFilePath)
    {
        var xsl = new System.Xml.Xsl.XslCompiledTransform();
        xsl.Load(xsltFilePath);

        using (var reader = XmlReader.Create(xmlFilePath))
        using (var writer = XmlWriter.Create(outputFilePath))
        {
            xsl.Transform(reader, writer);
        }

        Console.WriteLine($"File transformed: {xmlFilePath} -> {outputFilePath}");
    }
}