<script setup lang="ts">
import type { NewsItemDto } from '@/types/NewsItemDto'
import { computed } from 'vue'

const props = defineProps<{
  newsItem: NewsItemDto
}>()

const formattedDate = computed(() => {
  const date = new Date(props.newsItem.date)
  return date.toLocaleDateString('en-GB', {
    day: 'numeric',
    month: 'long',
    year: 'numeric',
  })
})
</script>

<template>
  <div class="news-item">
    <div v-if="newsItem.imageUrl" class="news-image">
      <img :src="newsItem.imageUrl" alt="News image" />
    </div>
    <div class="news-content">
      <div class="news-date">{{ formattedDate }}</div>
      <h2 class="news-title">{{ newsItem.title }}</h2>
      <p class="news-description">{{ newsItem.content }}</p>
    </div>
  </div>
</template>

<style scoped>
.news-item {
  max-width: 800px;
  margin: 2rem auto;
  padding: 1.5rem;
  border: 1px solid #ddd;
  border-radius: 8px;
  background-color: #fff;
  box-shadow: 0 2px 10px 0 rgba(34, 60, 80, 0.1);
  display: flex;
  flex-direction: row;
  gap: 1rem;
}

.news-image img {
  max-width: 360px;
  border-radius: 4px;
  object-fit: cover;
}

.news-content {
  text-align: left;
}

.news-date {
  font-weight: 600;
  color: #e60000;
  font-size: 0.9rem;
  margin: 20px 0;
}

.news-title {
  font-size: 1.5rem;
  font-weight: 700;
  margin: 0.5rem 0;
  color: #222;
}

.news-description {
  font-size: 1rem;
  line-height: 1.6;
  color: #444;
}
</style>
