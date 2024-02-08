class GameEngine {
  constructor() {
    this.roundNumber = 0;
    this.rounds = [];
    this.updateRound();
    this.matchId = 0;
    this.roundId = 0;
    this.score = {
      player1: 0,
      player2: 0,
    };
    this.roundStatus = 0; /* 0 -> P1 dont move it, 1 -> P1 move it ist time to P2 -> 2, both movements done */

    this.selectionFromPlayer = this.selectionFromPlayer.bind(this);
    this.showWinner = this.showWinner.bind(this);
    this.ronda = new Ronda();
  }
  updateRound() {
    document.getElementById("round-number").innerHTML = this.roundNumber;
  }
  showForm() {
    this.createGame();
  }
  reset() {
    this.score = {
      player1: 0,
      player2: 0,
    };
    this.rounds = [];
    this.roundNumber = 0;
    this.updateRound();
    this.showRounds();
  }
  createGame() {
    this.reset();
    fetch("https://localhost:7024/game/create", {
      method: "POST",
      headers: {
        "Content-Type": "application/x-www-form-urlencoded",
      },
      body:
        "player1=" + encodeURIComponent(Player1.id) + "&player2=" + encodeURIComponent(Player2.id),
    })
      .then((response) => {
        if (!response.ok) {
          console.error("Error al enviar datos al servidor.");
          return;
        }
        response.json().then((obj) => {
          this.matchId = obj.id;
          this.hideForm();
          this.showGameZone();
          this.ronda.createRound(this.player1.id, this.player2.id, this.matchId);
          this.player1.enableSelect();
        });
      })
      .catch((error) => {
        console.error("Error al enviar datos al servidor.", error);
      });
  }
  hideForm() {
    document.getElementsByClassName("register-zone")[0].style.display = "none";
  }
  showGameZone() {
    document.getElementsByClassName("play-zone")[0].style.display = "flex";
  }
  selectionFromPlayer(id, selection) {
    if (id == this.player1.id && this.ronda.status.player1 == false) {
      this.ronda.status.player1 = true;
      this.ronda.selectionPlayer(id, selection);
      this.player2.enableSelect();
    }
    if (id == this.player2.id && this.ronda.status.player1 == true) {
      this.ronda.status.player2 = true;
      this.ronda.selectionPlayer(id, selection);
    }
  }
  showWinner() {
    if (this.winner == 1) {
      document.getElementById("Winner").innerHTML = `¡El ganador es ${this.player1.fullName}!`;
    } else if (this.winner == 2) {
      document.getElementById("Winner").innerHTML = `¡El ganador es ${this.player2.fullName}!`;
    } else {
      document.getElementById("Winner").innerHTML = `¡Hay un empate!`;
    }

    document.getElementById("win-audio").play();
    document.getElementsByClassName("crown")[0].style.display = "flex";
    setTimeout(() => {
      this.roundNumber++;
      document.getElementsByClassName("crown")[0].style.display = "none";
      this.setRound();
      this.showRounds();
      this.player1.unselect();
      this.player2.unselect();
      document.getElementById("win-audio").stop();
    }, 2000);
  }
  getPlayer1(player) {
    this.player1 = player;
  }
  validateScore() {
    if (this.winner == 1) {
      this.score.player1++;
    } else if (this.winner == 2) {
      this.score.player2++;
    }

    if (this.score.player1 >= 3 || this.score.player2 >= 3) {
      alert("Se acabo el juego");
      gameOver();
    }
  }
  setRound() {
    this.ronda = new Ronda();
    this.ronda.createRound(this.player1.id, this.player2.id, this.matchId);
    this.player1.enableSelect();
    this.rounds.push(this.winner);
    this.validateScore();
    this.roundNumber++;
    this.winner = 0;
  }
  getPlayer2(player) {
    this.player2 = player;
  }
  showRounds() {
    document.getElementsByClassName("play-zone-top-round-history")[0].innerHTML = "";
    this.rounds.forEach((round) => {
      document.getElementsByClassName(
        "play-zone-top-round-history"
      )[0].innerHTML += `<div class="play-zone-top-round-history-box history-${round}"><p>${round}</p></div>`;
    });
  }
  verifyRondas() {
    this;
  }
}
const Game = new GameEngine();

function gameOver() {
  document.getElementById("GameOver").style.display = "flex";
  document.getElementsByClassName("play-zone")[0].style.display = " none";

  document.getElementById("restart").addEventListener("click", () => {
    Game.createGame();
  });
  document.getElementById("reboot").addEventListener("click", () => {
    window.location.reload();
  });
}
