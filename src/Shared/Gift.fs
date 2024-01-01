namespace Shared

open System

[<AutoOpen>]
type Gift =
    { Id: Guid
      Person: Person
      Name: string
      Description: string option
      Url: string option
      Image: byte array
      Price: decimal option }

type GiftReservation =
    { Id: Guid
      Gift: Gift
      ReservedBy: Person
      ReservedAt: DateTimeOffset }

module GiftReservation =
    let create (gift: Gift) (person: Person) =
        { Id = Guid.NewGuid()
          Gift = gift
          ReservedBy = person
          ReservedAt = DateTimeOffset.Now }

[<AutoOpen>]

type IGiftApi =
    { gifts: unit -> Async<Gift list>
      gift: Guid -> Async<Gift option>
      addGift: Gift -> Async<Gift>
      updateGift: Gift -> Async<Gift>
      deleteGift: Guid -> Async<unit>
      reserveGift: GiftReservation -> Async<GiftReservation>
      unreserveGift: Guid -> Async<unit> }