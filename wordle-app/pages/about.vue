<template>
	<v-container class="text-center">
		<v-card class="mx-auto" max-width="800" outlined>
			<v-card-title class="text-h5" >Instructions</v-card-title>
			<v-card-text>
				<div class="text-body-1 mb-3">
					Welcome to Timothy Nelson's final project for CSCD 379, Spring 2024 Quarter<br />
					I chose to make a simple slot machine, with 3 reels<br />
					Then I used the database to store the user's current coin count, and you start with 1000.<br />
					Each spin costs 10,20,30,40, or 50 coins, and prizes will be multiplied if you bet higher <br />
					The odds for each (per reel) and their prizes are:<br />
					<v-icon class="mr-2">mdi-fire</v-icon>Wild: 10% - 750 coins<br />
					<v-icon class="mr-2">mdi-bitcoin</v-icon>Coin: 5% - 1000 coins<br />
					<v-icon class="mr-2">mdi-star</v-icon>Star: 10% - 500 coins<br />
					<v-icon class="mr-2">mdi-numeric-7</v-icon>Seven: 15% - 100 coins<br />
					<v-icon class="mr-2">mdi-chess-king</v-icon>King: 20% - 20 coins<br />
					<v-icon class="mr-2">mdi-chess-queen</v-icon>Queen: 20% - 10 coins<br />
					<v-icon class="mr-2">mdi-chess-knight</v-icon>Jack: 20% - 5 coins<br />
				</div>
			</v-card-text>

			<v-card-actions>
				<v-spacer></v-spacer>
				<v-btn color="primary" variant="flat" @click="router.push('/')">Home</v-btn>
			</v-card-actions>
		</v-card>
		
	</v-container>
</template>

<script setup lang="ts">
const router = useRouter();
</script>
