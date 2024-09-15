# PokeFun

<p>Welcome to Poke fun, this is a fun project that uses :</p>

<p>pokeapi.co and api.funtranslations.com/translate/ ( Thanks to them ).</p>

<p>You can call the Api with a pokemon name and it will return some informations about it.
If you want to add some sprinkles to it, you can call the translate endpoint and base on 
__habitat__ and __is_legendary__ conditions it will do yoda or shakespear translations :D</p>


## How to run :)


<p>Most basic way to run the api in a self contained way is by using Docker.<br> 

Install docker desktop and then by opening your terminal run following commands :<br>

`docker image pull gabrieletoffanin/pokefun:latest`<br>

`docker run -it --rm -p 5000:8080 --name pokefunapi gabrieletoffanin/pokefun:latest`<br>

at the moment just Http is supported. In a production environment based on the host i would suggest to also enable https.
</p>

### Example

localhost:5000/pokemon/mewtwo

localhost:5000/pokemon/translated/mewtwo

## Production Suggestions

<p>I would for sure cash the pokemon __species__ api response, as i don't think it will change often. <br>
Also as i noted before https would be required if for example the choosed deploy environment would be an Azure App Service. <br>
