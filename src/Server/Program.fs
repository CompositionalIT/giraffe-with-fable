open Saturn
open Giraffe
open Giraffe.ViewEngine

let view: HttpHandler =
    html [] [
        head [] [ script [ _type "module"; _src "app.js" ] [] ]
        body [] [
            div [] [
                p [] [ str (Shared.Say.hello "isaac") ]
                button [ _id "btnClick" ] [ str "Click me" ]
            ]
        ]
    ]
    |> htmlView

let app = application {
    use_router view
    use_static "dist"
}

run app