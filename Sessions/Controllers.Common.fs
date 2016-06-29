﻿module Controllers.Common //TODO better name?

open System
open System.Net
open System.Net.Http
open System.Web.Http

let Catch (c: ApiController) successCode op =
    try
        c.Request.CreateResponse(successCode, op())
    with
    | ex ->
        c.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message)



