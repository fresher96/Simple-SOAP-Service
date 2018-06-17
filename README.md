
### General Notes
Person class on the client side changed the access modifiers of the properties. Constructors or methods were not generated. Even the logic of the setters and getters is different from the one written on the server side.

Apparently, logic can't be transfered through the protocol but only data; the logic must be executed on the server using a defined `WebMethod`. And for example, something like
```
Person person = ...
int age = person.getAge();
```
would call the generated `getAge` in the client not the one implemented in the server. As opposed to calling a `WebMethod` like `HelloWorld`.  

