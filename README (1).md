
# Rock Paper Scissors

## About the app

The project is a web application for playing the game Rock, Paper, Scissors. Users can register two players, who then take turns making moves. The game proceeds until one player wins three rounds. The application displays the partial results of each round and declares the winner or if there's a tie. Users have the option to repeat or start a new game with new players. The application is built using ASP.NET and SQL Server, ensuring proper handling of errors, responsive design, and adherence to SOLID principles.



## Demo

Insert gif or link to demo

## Usage ( step by step )
1. clone the repository
2. You need to have at least .Net 8 version 
3. Run it!
## API Reference

### -> Create User

```http
  POST /CreatePlayer
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `name` | `string` | **Required**. User name |
| `last_name` | `string` | **Required**. Video that will be combinated |


#### Response

The response to the create player requests may vary between:

| Status | JSON     | Example                | Description |
| :-------- | :------- | :------------------------- | :-------
| `200` | `✅` | Returns { id: **number**} | it's user ID
| `400` | `✅` | Returns { description: "**bad request**"} | request is invalid

#### Information
This endpoint will create a user in the DB, after a validation, if something goes wrong, it will return a 400 status code.


### -> Create Round

```http
  POST /Round/Create
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `playerID1` | `int` | **Required**. ID of player 1. |
| `PlayerID2` | `int` | **Required**. ID of player 2.|
| `MatchId` | `int` | **Required**. Id of the match|


#### Response

The response to the create player requests may vary between:

| Status | JSON     | Example                | Description |
| :-------- | :------- | :------------------------- | :-------
| `200` | `✅` | Returns { id_Ronda: number } | ID of the created round.
| `400` | `✅` | Returns { description: "error description" } | Error description in case of failure.

#### Information
This endpoint creates a new round in the database. It requires the IDs of two registered players. If one or both of the players are not registered, a 400 status code is returned along with an error description. If the round creation is successful, a 200 status code is returned along with the ID of the created round.


### -> Register Movement

```http
  POST /RegisterMovement
```

### Request Parameters

| Parameter    | Type   | Description                                                  |
|--------------|--------|--------------------------------------------------------------|
| `playerID`   | `int`  | **Required**. ID of the player making the movement.          |
| `movementID` | `int`  | **Required**. ID of the movement performed by the player (1, 2, or 3). |
| `roundID`    | `int`  | **Required**. ID of the round in which the movement is being registered. |

### Responses

| Código de Estado | Descripción                              | Ejemplo de Respuesta JSON                                                                   |
|------------------|------------------------------------------|--------------------------------------------------------------------------------------------|
| `200`            | Operación exitosa                        | `{"description": "La ronda ha sido modificada exitosamente."}`                              |
| `400`            | Error al modificar la ronda              | `{"description": "Error al modificar la ronda` |
| `400`            | El jugador no existe                    | `{"description": "El jugador no existe"}`                                                   |
| `400`            | El movimiento no es válido              | `{"description": "El movimiento no es válido"}`                                              |
| `400`            | Movimiento ya registrado para esta ronda | `{"description": "Movimiento ya registrado para esta ronda"}`                               |
| `400`            | La ronda no está registrada             | `{"description": "La ronda no está registrada"}`                                             |


#### Information

This endpoint allows registering a player's movement in a specific round.

## Features

- .NET
- SQL 
- JS / HTML / Css
- Cross platform


## Authors

- [@Fscripter](https://www.github.com/fscripter)
