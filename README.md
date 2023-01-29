# HttpClientDemo
An ASP.NET MVC project to demonstrate how to use `HttpClient` correctly.

The application uses the [Dad Joke API](https://rapidapi.com/KegenGuyll/api/dad-jokes) to get some information on the screen. 
This API uses an API-Key that I stored as a client secret.

The `HttpClient` is used to call this API, which of course is a common practise. 
And since `HttpClient` implements `HttpMessageInvoker`, which in turn implements `IDisposable`, we put the `HttpClient` instantiation in a `using` declaration, right? 
Or should we?

This project will contain a number of branches for each stage I want to explain:
- The main branch contains the initial version of the application that instantiates a separate `HttpClient` for each API call I make.
- The next branch fixes an improper instantiation antipattern on `HttpClient`: you should instantiate `HttpClient` only once and share it among the sessions.<br/>
  Simon Timms wrote an article called [You're using HttpClient wrong ant it is destabilizing your software](https://www.aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/) 
  about this showing that using this pattern, at some point the available sockets the HttpClient can use will be exhausted, causing severe performance issues. 
  Apart from that, re-using the HttpClient gives you an immediate performance boost.<br/> There's even a [Patterns and Practices document](https://learn.microsoft.com/en-us/azure/architecture/antipatterns/improper-instantiation/) about it on Microsoft Learn.
- Next up, we use the `IHttpClientFactory` for creating the `HttpClient` for us. This has a number of benefits. 
  For instance, it is resilient against dns changes due to dns TTL expiration, which could change the IP address for the domain name. 
  Another benefit is you can define a typed `Httpclient` that will be used for a specific type that uses the `HttpClient`.
- The fourth branch contains a typed `HttpClient` implementation as well, but this time it is a *named client* that can be instantiated where neccesary. 
  The benefit is that this named client can be used in different places, but you could also use different `HttpClient`s in a class without having to inject multiple typed `HttpClient`s.