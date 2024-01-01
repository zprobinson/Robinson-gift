[<AutoOpen>]
module GiftApi

open Shared

type GiftApi() =
    member this.gifts() = async { return [] }

    member this.gift id = async { return None }

    member this.addGift gift = async { return gift }

    member this.updateGift gift = async { return gift }

    member this.deleteGift id = async { return () }

    member this.reserveGift reservation = async { return reservation }

    member this.unreserveGift id = async { return () }

    member this.build() : IGiftApi =
        { gifts = this.gifts
          gift = this.gift
          addGift = this.addGift
          updateGift = this.updateGift
          deleteGift = this.deleteGift
          reserveGift = this.reserveGift
          unreserveGift = this.unreserveGift }