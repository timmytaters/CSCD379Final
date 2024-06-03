<template>
  <v-container>

    <v-text>Tim Final</v-text>
    
  </v-container>
  <v-card>
    <p>Current Coins: {{ CurrentCoins }}</p>
    <p>You Won: {{ WinAmount }}</p>
    <div class="slot-machine">
      <div class="box" v-for="(content, index) in boxes" :key="index">
        {{ content }}
      </div>
    </div>
    <v-btn color="primary" variant="flat" @click="spin(10)">10</v-btn>
    <v-btn color="primary" variant="flat" @click="spin(20)">20</v-btn>
    <v-btn color="primary" variant="flat" @click="spin(30)">30</v-btn>
    <v-btn color="primary" variant="flat" @click="spin(40)">40</v-btn>
    <v-btn color="primary" variant="flat" @click="spin(50)">50</v-btn>
  </v-card>
</template>

<script setup lang="ts">
import { Symbols } from "~/scripts/symbols";
const boxes = ref(["Box 1", "Box 2", "Box 3"]);
const router = useRouter();
var CurrentCoins = 1000;
var WinAmount = 0;

const isPressed = ref(false);

function pressEffect() {
  isPressed.value = true;
}

function releaseEffect() {
  isPressed.value = false;
}

function spin(bet: number){
  if(bet > CurrentCoins){
    WinAmount = "Out of Coins :(";
    return;
  }
  CurrentCoins -= bet;
  var symbol1 = Symbols[Math.floor(Math.random() * 100)];
  var symbol2 = Symbols[Math.floor(Math.random() * 100)];
  var symbol3 = Symbols[Math.floor(Math.random() * 100)];
  boxes.value[0] = symbol1;
  boxes.value[1] = symbol2;
  boxes.value[2] = symbol3;
  //number win = checkResult(symbol1, symbol2, symbol3);
  //CurrentCoins += win*bet;
}

function checkResult(s1: string, s2: string, s3:string){
  if(s1 == s2 == s3){
    return prize(s1);
  }
}

function prize(symbol: string){
  switch(symbol){
    
  }
}

</script>

<style scoped>
  .slot-machine {
    display: flex;
    justify-content: center; /* Center the boxes horizontally */
    align-items: center; /* Center the boxes vertically */
    height: 100vh; /* Make the container full height of the viewport */
  }

  .box {
    width: 100px; /* Set the width of each box */
    height: 100px; /* Set the height of each box */
    border: 1px solid #ccc;
    padding: 20px;
    margin: 10px;
    text-align: center;
  }
</style>