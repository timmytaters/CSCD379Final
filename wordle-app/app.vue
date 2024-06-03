<template>
  <NuxtLayout>
    <v-app :style="gradientBaseStyle" class="full-page">
      <v-app-bar color="primary" :elevation="2">
        <v-app-bar-title @click="$router.push('/')" style="cursor: pointer">
          Tim Slot Machine
        </v-app-bar-title>
        <v-btn @click="toggleDrawer" icon="mdi-menu"></v-btn>
      </v-app-bar>

      <v-navigation-drawer
        v-model="drawer"
        location="right"
        disable-resize-watcher
      >
        <v-list>
          <v-list-item @click="router.push('/Leaderboard')" class="text-center">
          <v-icon class="mr-2">mdi-trophy</v-icon>
            Leaderboard
          </v-list-item>
        </v-list>

        <v-list>
          <v-list-item @click="router.push('/About')" class="text-center">
          <v-icon class="mr-2">mdi-book-open</v-icon>
            Instructions
          </v-list-item>
        </v-list>

      </v-navigation-drawer>
      <v-main>
        <NuxtPage />
      </v-main>
    </v-app>
  </NuxtLayout>
</template>

<script setup lang="ts">
import nuxtStorage from "nuxt-storage";


const router = useRouter();
var drawer = ref(false);
function toggleDrawer() {
  drawer.value = !drawer.value;
}
</script>

