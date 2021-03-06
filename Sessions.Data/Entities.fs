﻿module Entities

open System
open Dapper.Contrib.Extensions

// Note: CLIMutable attribute seems to be required in some cases, but not all cases. 
// If you get an error asking for a certain constructor, or a wierd error, try adding the attribute

[<CLIMutable>]
[<Table("handles")>]
type Handle = 
    { Id : int
      ProfileId : Guid 
      Type : string
      Identifier : string }

[<Table("profiles")>]
type Profile = 
    { Id : Guid
      Forename : string
      Surname : string
      ImageUrl : string
      Bio : string 
      IsAdmin : bool }

[<CLIMutable>]
[<Table("notes")>]
type Note = 
    { [<Key>] Id : Guid
      SessionId : Guid
      DateAdded : DateTime
      DateModified : DateTime 
      Note : string }

[<CLIMutable>]
[<Table("sessions")>]
type Session = 
    { Id : Guid
      Title : string
      Description : string
      Status : string
      SpeakerId : Guid
      AdminId : Nullable<Guid>
      EventId : Nullable<Guid>
      DateAdded : DateTime }

[<CLIMutable>]
[<Table("events")>]
type Event =
    { Id : Guid
      Date : Nullable<DateTime>
      Name : string 
      MeetupEventId : Nullable<Guid> }

[<CLIMutable>]
[<Table("meetupEvents")>]
type MeetupEvent =
    { Id : Guid
      EventId : Guid
      MeetupId : string 
      PublishedDate : Nullable<DateTime>
      MeetupUrl : string }
