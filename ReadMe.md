## Victorian Plumbing Technical Test
This repository follows a technical specification supplied by Victorian Plumbing, requiring a single API end-point to create a customer order.

## About Application

This application is written in .NET 6.0 with C# using WebAPI technology. The project uses the older controller layout supplied by Microsoft as a base, using composition with Dependency Injection, over inheritence chaining. The project layout follows the Clean Architecture principles (aka Onion Architecture) and Domain-Driven design principles. Within each layer, services and processes are encapsulated in domain-feature folders. The application contains 4 layers:

- API (Presentation layer)
- Application
- Domain
- Infrastructure

There is another layer, which contains contract information (request and response data).
Dependency injection information for each layer can be found within the DependencyInjectionRegister class on each layer, where applicable.

This application has scaffoliding for Microsoft SQL Server support, using Entity Framework Core. This has been commented out, in order to better allow for testing on local environments.


## Testing
Testing is held within it's own folder, encapsulating a collection of unit tests. These tests cover some of the given validation logic and a service related to building connection strings.
The application makes use of XUnit as it's test runner, alongside the Fluent Assertions package, to allow for more readable test assertions.
The usage of XUnit is to allow for stronger integration tests in the future, using the WebApplicationFactory process, to test API calls locally.

## Future Considerations
As mentioned, there is scaffolding for EFCore done on this application. This requires further testing and integration with a representative environment.
A number of domain objects have been annotated across the application, to try and inform future users about decisions and extensions, which would require stakeholder input. These include:
- The representation of pricing elements for ordered products
- The need for supporting multiple currencies within a single order basket
- The need to support percentage deposits from the front-end application
- The ability to validate if an ordered product can be fulfilled (in the case of no stock remaining / discontinued products)
- Any type of message submissions (using something like an event bus or micro-service) to send confirmation emails to customers
  - This could be done with a number of transport technologies, but would be advisable to integrate with anything the business already uses to leverage existing processes
- Any type of integration with a payment gateway (if desired here) in the case the creation of the order fails. This would require an extension of supplied payment information
- Any type of tax validation, which may need to take into account the type of customer placing the given order
- It has been assumed that if we can't validate customer details, we should reject an order, as don't want to create anonymous purchases
- There has been no consideration made for how public this endpoint is. Security could be added depending on where the API is hosted, or could integrate a type of token provisioner
  - This may want to have stronger consideration due to the potential exposure of PII (Email address, customer delivery address, name, phone number etc)
