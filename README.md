C# Asynchronous Pluggable Protocol Handler
==========================================

Handle custom protocol schemes when you are using the `WebBrowser` control in a Windows Forms (or WPF) desktop application.

```C#
ProtocolFactory.Register("rsrc", () => new ResourceProtocol());
var html = @"<html><body style=""background-image: url(rsrc:helloworld.png)""></body></html>";
Browser.DocumentText = html;
```

The `ResourceProtocol` class in the example above implements the the following interface to deliver embedded resources to the browser control:

```C#
public interface IProtocol
{
    string Name { get; }
    Task<Stream> GetStreamAsync(string url);
}
```

License
-------

[MIT X11](http://en.wikipedia.org/wiki/MIT_License)
