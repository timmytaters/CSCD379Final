<template>
  <v-container>
    <v-text>Tim Final <br />Note: Minor flash warning</v-text>
  </v-container>
  <v-card>
    <div>
      <p> Current User : {{ username }}</p>
    </div>
    <input
      type="text"
      v-model="usernameInput"
      @keyup.enter="saveUsername"
      placeholder="Enter your username"
    />
    <p> {{ errorMessage }}</p>
    <div class="text-container">
      <p>Current Coins: {{ currentCoins }}</p>
      <p :style="{ color: winColor }">You Won: {{ winAmount }}</p>
    </div>
    <div class="slot-machine">
      <v-icon class="box" v-for="(icon, index) in boxes" :key="index">{{ icon }}</v-icon>
    </div>
    <div class="buttons-container">
      <v-btn color="primary" @click="spin(10)" :disabled="busy">Bet 10</v-btn>
      <v-btn color="primary" @click="spin(20)" :disabled="busy">Bet 20</v-btn>
      <v-btn color="primary" @click="spin(30)" :disabled="busy">Bet 30</v-btn>
      <v-btn color="primary" @click="spin(40)" :disabled="busy">Bet 40</v-btn>
      <v-btn color="primary" @click="spin(50)" :disabled="busy">Bet 50</v-btn>
    </div>
    <div class="speed-buttons-container">
      <v-btn color="primary" @click="setSpeed(3)">Fast Mode</v-btn>
      <v-btn color="primary" @click="setSpeed(2)">Normal Mode</v-btn>
      <v-btn color="primary" @click="setSpeed(1)">Slow Mode</v-btn>
    </div>
    <div class="saveload-buttons-container">
      <v-btn color="primary" @click="postCoins()">Save</v-btn>
      <v-btn color="primary" @click="getCoins()">Load</v-btn>
    </div>
  </v-card>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { Symbols } from '~/scripts/symbols';
import Axios from 'axios'
import type { Player } from "../scripts/player";
const usernameInput = ref("");
const router = useRouter();
const currentCoins = ref(1000);
const winAmount = ref("0");
const speed = ref(1000);
const winColor = ref("white");
const boxes = ref(["mdi-loading", "mdi-loading", "mdi-loading"]);
const player = ref<Player>();
const busy = ref(false);
const errorMessage = ref("");
var username = ref("");

function setSpeed(mode: number) {
  switch(mode) {
    case 1:
      speed.value = 1000;
      break;
    case 2:
      speed.value = 500;
      break;
    case 3:
      speed.value = 0;
      break;
    default:
      speed.value = 1000;
      break;
  }
}

async function spin(bet: number) {
  if (bet > currentCoins.value) {
    errorMessage.value = "Error: Out of Coins :(";
    return;
  }
  busy.value = true;
  currentCoins.value -= bet;
  boxes.value[0] = "mdi-loading";
  boxes.value[1] = "mdi-loading";
  boxes.value[2] = "mdi-loading";
  await sleep(speed.value);
  const symbol1 = Symbols[Math.floor(Math.random() * 100)];
  boxes.value[0] = iconForSymbol(symbol1);
  await sleep(speed.value);
  const symbol2 = Symbols[Math.floor(Math.random() * 100)];
  boxes.value[1] = iconForSymbol(symbol2);
  await sleep(speed.value);
  const symbol3 = Symbols[Math.floor(Math.random() * 100)];
  boxes.value[2] = iconForSymbol(symbol3);
  const win = checkResult(symbol1, symbol2, symbol3);
  currentCoins.value += win * bet;
  winAmount.value = (win * bet).toString();
  flashWin(win);
  busy.value = false;
}

function checkResult(s1: string, s2: string, s3: string) {
  if (s1 === s2 && s2 === s3) {
    return prize(s1);
  } else if (s1 === s2 && s3 === "Wild") {
    return prize(s1);
  } else if (s1 === s3 && s2 === "Wild") {
    return prize(s1);
  } else if (s2 === s3 && s1 === "Wild") {
    return prize(s2);
  } else if (s1 === "Wild" && s2 === "Wild") {
    return prize(s3);
  } else if (s1 === "Wild" && s3 === "Wild") {
    return prize(s2);
  } else if (s2 === "Wild" && s3 === "Wild") {
    return prize(s1);
  } else {
    return 0;
  }
}

function prize(symbol: string) {
  switch (symbol) {
    case "Wild":
      return 750;
    case "Coin":
      return 1000;
    case "Star":
      return 500;
    case "Seven":
      return 100;
    case "King":
      return 20;
    case "Queen":
      return 10;
    case "Jack":
      return 5;
    default:
      return 0;
  }
}

async function flashWin(size: number) {
  if (size >= 500) {
    for (let i = 0; i < 4; i++) {
      winColor.value = "yellow";
      await sleep(100);
      winColor.value = "white";
      await sleep(100);
    }
  } else if (size >= 100) {
    for (let i = 0; i < 2; i++) {
      winColor.value = "yellow";
      await sleep(500);
      winColor.value = "white";
      await sleep(500);
    }
  } else if (size > 0) {
    winColor.value = "yellow";
    await sleep(1000);
    winColor.value = "white";
  } else {
    winColor.value = "white";
  }
}

function sleep(ms: number): Promise<void> {
  return new Promise((resolve) => setTimeout(resolve, ms));
}

function iconForSymbol(symbol: string): string {
  switch (symbol) {
    case "Wild":
      return "mdi-fire";
    case "Coin":
      return "mdi-bitcoin";
    case "Star":
      return "mdi-star";
    case "Seven":
      return "mdi-numeric-7";
    case "King":
      return "mdi-chess-king";
    case "Queen":
      return "mdi-chess-queen";
    case "Jack":
      return "mdi-chess-knight";
    default:
      return "mdi-help-circle";
  }
}

function postCoins(){
  if(username.value == ""){
    errorMessage.value = "Please sign in first"
    return;
  }
  Axios.post("Player/AddPlayer", {
    Name: username.value,
    Coins: currentCoins
  })
  .then(res => {
    console.log(res.data);
  })
  .catch(err => {
    errorMessage.value = err;
  })
  errorMessage.value = "Saved";
}

async function getCoins(){
  if(username.value == ""){
    errorMessage.value = "Please sign in first"
    return;
  }
  let url = "/Player/GetPlayer?playername=";
  url += username;
  const response = await Axios.get(url);
  if(response.data == null){
    errorMessage.value = "No user data found"
    return;
  }
  player.value = response.data;
  currentCoins.value = player.value?.coins as number;
}

function saveUsername(){
  username.value = usernameInput.value;
}

</script>

<style scoped>
.text-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-bottom: 20px;
}

.slot-machine {
  display: flex;
  justify-content: center;
  align-items: center;
  margin-bottom: 20px;
}

.box {
  width: 100px;
  height: 100px;
  border: 1px solid #ccc;
  padding: 20px;
  margin: 10px;
  text-align: center;
}

.buttons-container, .speed-buttons-container, .saveload-buttons-container {
  display: flex;
  justify-content: center;
  margin-top: 10px;
}

.buttons-container > * {
  margin: 0 5px;
}

.speed-buttons-container > * {
  margin: 0 5px;
}
</style>
