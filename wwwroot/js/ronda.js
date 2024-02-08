class Ronda {
  constructor() {
    this.id;
    this.status = {
      player1: false,
      player2: false,
    };
  }
  createRound(IdPlayer1, IdPlayer2, matchID) {
    fetch("https://localhost:7024/Round/Create", {
      method: "POST",
      headers: {
        "Content-Type": "application/x-www-form-urlencoded",
      },
      body:
        "player1=" +
        encodeURIComponent(IdPlayer1) +
        "&player2=" +
        encodeURIComponent(IdPlayer2) +
        "&matchId=" +
        encodeURIComponent(matchID),
    })
      .then((response) => {
        if (!response.ok) {
          console.error("Error al enviar datos al servidor.");
          return;
        }
        response.json().then((obj) => {
          this.id = obj.id;
        });
      })
      .catch((error) => {
        console.error("Error al enviar datos al servidor.", error);
      });
  }
  selectionPlayer(idPlayer, selection) {
    fetch("https://localhost:7024/Round/RegisterMovement", {
      method: "POST",
      headers: {
        "Content-Type": "application/x-www-form-urlencoded",
      },
      body:
        "playerID=" +
        encodeURIComponent(idPlayer) +
        "&movementID=" +
        encodeURIComponent(selection) +
        "&roundID=" +
        encodeURIComponent(this.id),
    })
      .then((response) => {
        if (!response.ok) {
          console.error("Error al enviar datos al servidor.");
          return;
        }
        response.json().then((obj) => {
          this.verify();
        });
      })
      .catch((error) => {
        console.error("Error al enviar datos al servidor.", error);
      });
  }
  verify() {
    console.log(this.status);
    if (this.status.player1 && this.status.player2) {
      this.getWinner();
      this.status.player1 = false;
      this.status.player2 = false;
    }
  }
  getWinner() {
    fetch("https://localhost:7024/Game/GetWinner", {
      method: "POST",
      headers: {
        "Content-Type": "application/x-www-form-urlencoded",
      },
      body: "roundId=" + encodeURIComponent(this.id),
    })
      .then((response) => {
        if (!response.ok) {
          console.error("Error al enviar datos al servidor.");
          return;
        }
        response.json().then((obj) => {
          this.winner = obj.winner;
          Game.winner = this.winner;
          Game.showWinner();
        });
      })
      .catch((error) => {
        console.error("Error al enviar datos al servidor.", error);
      });
  }
}
