class Player {
  constructor(prior) {
    this.name;
    this.lastName;
    this.roomID;
    this.id;
    this.isRegistered = false;
    this.prior = prior;
    this.idName = `player-${prior}`;
    this.mainDiv = document.getElementById(this.idName);

    this.setEvents();
    this.getData = this.getData.bind(this);
    this.hide();
    this.elements = ["piedra", "papel", "tijera "];
    this.selected = this.selected.bind(this);
    this.controller = new AbortController();
  }
  validateData() {
    this.createPlayerInDB();
  }
  getData() {
    this.name = document.getElementById(`${this.idName}-nombre`).value;
    this.lastName = document.getElementById(`${this.idName}-apellido`).value;
    this.fullName = this.name + " " + this.lastName;
  }
  setEvents() {
    document.getElementById(`${this.idName}-form`).addEventListener("submit", (e) => {
      e.preventDefault();

      this.getData();
      this.validateData();
    });
  }
  createPlayerInDB() {
    fetch("https://localhost:7024/player/create", {
      method: "POST",
      headers: {
        "Content-Type": "application/x-www-form-urlencoded",
      },
      body:
        "last_name=" + encodeURIComponent(this.lastName) + "&name=" + encodeURIComponent(this.name),
    })
      .then((response) => {
        if (!response.ok) {
          console.error("Error al enviar datos al servidor.");
          return;
        }
        this.isRegistered = true;
        response.json().then((obj) => {
          this.id = obj.id;
          this.activateAnother();
          document.getElementById(this.idName).classList.add("mobile-unplug");
          this.showName();
          this.hideForm();
        });
      })
      .catch((error) => {
        console.error("Error al enviar datos al servidor.", error);
      });
  }
  hide() {
    document.getElementById(this.idName).style.display = "none";
  }
  hideForm() {
    document.getElementById(`${this.idName}-form`).style.display = "none";
  }
  showForm() {
    document.getElementById(this.idName).style.display = "flex";
  }
  get2Player(player) {
    this.anotherPlayer = player;
  }
  activateAnother() {
    if (this.anotherPlayer == undefined) return;
    this.anotherPlayer.showForm();
  }
  showName() {
    document.getElementById(
      `${this.idName}-nombre-active`
    ).innerHTML = `${this.name} ${this.lastName}`;
  }
  enableSelect() {
    this.elements.forEach(
      (element, index) => {
        document.getElementsByClassName(element)[this.prior - 1].addEventListener("click", () => {
          this.selected(index + 1);
          document
            .getElementsByClassName(element)
            [this.prior - 1].classList.add(`selected-${this.prior}`);
        });
      },
      { signal: this.controller }
    );
  }
  unselect() {
    this.elements.forEach((element, index) => {
      document
        .getElementsByClassName(element)
        [this.prior - 1].classList.remove(`selected-${this.prior}`);
    });
  }
  disableSelect() {
    this.controller.abort();
  }
  selected(number) {
    Game.selectionFromPlayer(this.id, number);
  }
}

const Player1 = new Player(1);
const Player2 = new Player(2);
Player1.showForm();
Player1.get2Player(Player2);
Player2.get2Player(Game);

Game.getPlayer1(Player1);
Game.getPlayer2(Player2);
