Snapshot
========

Take a snapshot in time of your instance. Useful for when you want to keep the original state of an instance the same, while a third party component might be altering it at some point in time.

By default, it will take a snapshot of public fields and properties.

```csharp
var person = new Person("Steven", "Thuriot");
person.Age = 27;

var snapshot = person.TakeSnapshot();

person.Age = 28;

Console.WriteLine("Person age: {0}", person.Age);
Console.WriteLine("Snapshot age: {0}", snapshot.Age);
```

Output:

```
Person age: 28
Snapshot age: 27
```
