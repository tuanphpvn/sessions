﻿module Entities

open System
open Dapper.Contrib.Extensions

// Note: CLIMutable attribute seems to be required in some cases, but not all cases. 
// If you get an error asking for a certain constructor, or a wierd error, try adding the attribute

[<Table("handles")>]
type Handle = 
    { ProfileId : Guid 
      Type : string
      Identifier : string }

[<Table("profiles")>]
type Profile = 
    { Id : Guid
      Forename : string
      Surname : string
      Rating : int
      ImageUrl : string
      Bio : string }

[<CLIMutable>]
[<Table("sessions")>]
type Session = 
    { Id : Guid
      Title : string
      Description : string
      Status : string
      SpeakerId : Guid
      AdminId : Nullable<Guid>
      DateAdded : DateTime
      Date : Nullable<DateTime> }