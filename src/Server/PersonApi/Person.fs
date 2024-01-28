[<AutoOpen>]
module PersonApi

open Shared
open Microsoft.Extensions.Logging

type PersonApi(logger: ILogger<PersonApi>) =
    member this.getPerson id = async {
        return
            Some
                { Id = System.Guid.NewGuid()
                  FirstName = "Zach"
                  LastName = "Robinson"
                  Birthday = System.DateOnly.MinValue }
    }

    member this.getPeople() = async { return [] }

    member this.addPerson person = async { return person }

    member this.updatePerson person = async { return person }

    member this.deletePerson id = async { return () }

    member this.build() : IPersonApi =
        logger.LogDebug "Building PersonApi"

        { getPerson = this.getPerson
          getPeople = this.getPeople
          addPerson = this.addPerson
          updatePerson = this.updatePerson
          deletePerson = this.deletePerson }