# PokeFun

[![Docker Image CI](https://github.com/GabrieleToffanin/PokeFun/actions/workflows/build-and-push-dockerimage.yml/badge.svg)](https://github.com/GabrieleToffanin/PokeFun/actions/workflows/build-and-push-dockerimage.yml)

<p>Welcome to Poke Fun, this is a fun project that uses :</p>

<p>pokeapi.co and api.funtranslations.com/translate/ ( Thanks to them ).</p>

<p>You can call the API with a Pokémon name, and it will return some information about it.
If you want to add some sprinkles to it, you can call the translate endpoint, and based on habitat and is_legendary conditions, it will do Yoda or Shakespeare translations.

## How to run :)

The most basic way to run the API in a self-contained way is by using Docker.

Install Docker Desktop, and then by opening your terminal, run the following commands:

```
docker image pull gabrieletoffanin/pokefun:latest
docker run -it --rm -p 5000:8080 --name pokefunapi gabrieletoffanin/pokefun:latest
```

At the moment, only HTTP is supported. In a production environment based on the host, I would suggest also enabling HTTPS.

Also there is no i386 docker manifest, so that architecture is not supported. ( Sorry 32 bit users :( )

### Example

localhost:5000/pokemon/mewtwo

localhost:5000/pokemon/translated/mewtwo

## Production Suggestions

I would for sure cache the Pokémon species API response, as I don’t think it will change often.<br>
Also, as I noted before, HTTPS would be required if, for example, the chosen deployment environment were an Azure App Service.<br>
I would also choose a private container registry, such as the Azure or GitHub one.<br>
I left Decide when no Specie Id can be found, actually an error or a default fallback would be necessary for what regards a production environment.
