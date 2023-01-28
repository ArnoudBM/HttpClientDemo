# HttpClientDemo
An ASP.NET MVC project to demonstrate how to use `HttpClient` correctly.

The application uses the [Dad Joke API](https://rapidapi.com/KegenGuyll/api/dad-jokes) to get some information on the screen. 
This API uses an API-Key that I stored as a client secret.

The `HttpClient` is used to call this API, which of course is a common practise. 
And since `HttpClient` implements `HttpMessageInvoker`, which in turn implements `IDisposable`, we put the `HttpClient` instantiation in a `using` declaration, right? 
Or should we?

This project will contain a number of branches for each stage I want to explain:
- The main branch contains the initial version of the application that instantiates a separate `HttpClient` for each API call I make.
