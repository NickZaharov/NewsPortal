<script setup lang="ts">
import { onMounted } from 'vue'
import NewsCard from '@/components/NewsCard.vue'
import SearchBar from '@/components/SearchBar.vue'
import { useNews } from '@/composables/useNews'

const { newsList, isLoading, fetchNews } = useNews()

onMounted(fetchNews)
</script>

<template>
  <div class="title-box container">
    <p class="title-box__title">News</p>
  </div>
  <div class="loader-container">
    <SearchBar @search="fetchNews" />
    <div v-if="isLoading">
      <div class="loading-spinner"></div>
    </div>
    <div v-else-if="newsList.length === 0">
      <p>Ничего не найдено</p>
    </div>
  </div>
  <NewsCard v-for="item in newsList" :newsItem="item" />
</template>

<style scoped>
.title-box__title {
  margin: 0;
  font-size: 3.75rem;
  font-weight: 700;
  position: relative;
  z-index: 1;
}

.title-box {
  background-image: url('https://cdn.pixabay.com/photo/2020/08/14/09/36/news-5487368_1280.jpg');
  background-size: cover;
  background-position: center;
  color: #fff;
  padding: 20px;
  text-align: center;
  background-color: #f5f5f5;
}

.container {
  height: 100px;
  line-height: 100px;
  position: relative;
  overflow: hidden;
}

.container::after {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(100, 100, 100, 0.5);
}

.loader-container {
  margin: 10px auto;
  display: flex;
  flex-direction: column;
  justify-content: center;
  max-width: 800px;
}

.loading-spinner {
  margin: 15px auto;
  width: 50px;
  height: 50px;
  border: 5px solid #f3f3f3;
  border-top: 5px solid #b71ef3;
  border-radius: 50%;
  animation: spin 1s linear infinite;
}

@keyframes spin {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}
</style>
