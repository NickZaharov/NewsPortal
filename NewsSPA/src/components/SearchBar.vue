<template>
  <input
    v-model="query"
    @input="onInput"
    placeholder="Введите слово или начальные буквы для поиска..."
    class="search-input"
  />
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { debounce } from 'lodash-es'

const emit = defineEmits(['search'])
const query = ref('')

const onInput = debounce(() => {
  emit('search', query.value.trim())
}, 200)
</script>

<style scoped>
.search-bar {
  position: relative;
  display: flex;
  align-items: center;
  max-width: 400px;
  margin: 0 auto;
}

.search-input {
  width: 100%;
  margin: 20px 0 0;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 8px;
  font-size: 16px;
  transition: border-color 0.3s;
}

.search-input:focus {
  outline: none;
  border-color: #007bff;
}
</style>
