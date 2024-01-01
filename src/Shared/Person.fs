namespace Shared

open System

[<AutoOpen>]
type Person =
    { Id: Guid
      FirstName: string
      LastName: string
      Birthday: DateOnly }

type IPersonApi =
    { getPeople: unit -> Async<Person list>
      getPerson: string -> Async<Person option>
      addPerson: Person -> Async<Person>
      updatePerson: Person -> Async<Person>
      deletePerson: string -> Async<unit> }