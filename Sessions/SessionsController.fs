﻿namespace Controllers

open System
open System.Net
open System.Net.Http
open System.Web.Http
open SessionsRepository
open DataTransform
open Models
open RestModels


type SessionsController() = 
    inherit ApiController()

    let patch (id: Guid) (op: PatchOp) =
        match op.Path.ToLowerInvariant() with
        | "description" | "title" -> updateField id op.Path op.Value
        | "eventid" -> 
            if String.IsNullOrWhiteSpace op.Value then
                updateField id op.Path None // Its acceptable to remove the eventId for a session
            else 
                match Guid.TryParse op.Value with
                | true, guid -> updateField id op.Path guid
                | false, _ -> raise <| Exception("Error: patch value could not be parsed as a guid. EventId must be a guid")                
        | _ ->  raise <| Exception(sprintf "Error: Patch currently does not accept: %s for session" op.Path)         

    member x.Post(session: Session) = (fun () -> session |> Session.toEntity |> add) |> Catch.respond x HttpStatusCode.Created 

    member x.Get() = (fun () -> getAll() |> Seq.map Session.toModel) |> Catch.respond x HttpStatusCode.OK 

    member x.Get(id : Guid) = (fun () -> get id |> Session.toModel) |> Catch.respond x HttpStatusCode.OK 

    [<Route("sessions/ids")>]
    [<HttpGet>]
    member x.GetSessionIdsByEventId(eventId : Guid) = 
        (fun () -> getIdsByEventId eventId) |> Catch.respond x HttpStatusCode.OK 

    [<HttpGet>]
    member x.GetByEventId(eventId : Guid) = (fun () -> getSessionsByEventId eventId) |> Catch.respond x HttpStatusCode.OK

    member x.Patch(id: Guid, op: PatchOp) = (fun () -> patch id op) |> Catch.respond x HttpStatusCode.NoContent 
