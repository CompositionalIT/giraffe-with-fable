/// Client-side-only code
module App

open Browser.Dom

document.getElementById("btnClick").onclick <- (fun _ -> window.alert (Shared.Say.hello "isaac"))