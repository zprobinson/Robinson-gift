namespace Shared

open System

[<AutoOpen>]
type Person =
    { Id: Guid
      FirstName: string
      LastName: string
      Birthday: DateOnly }

module Person =
    let name (person: Person) =
        sprintf "%s %s" person.FirstName person.LastName

type IPersonApi =
    { getPeople: unit -> Async<Person list>
      getPerson: string -> Async<Person option>
      addPerson: Person -> Async<Person>
      updatePerson: Person -> Async<Person>
      deletePerson: string -> Async<unit> }