import { ref } from 'vue'
import axios from 'axios'
import type { NewsItemDto } from '@/types/NewsItemDto'

const apiUrl = import.meta.env.VITE_API_URL
const newsEndpoint = `${apiUrl}/api/News`

export function useNews() {
  const newsList = ref<NewsItemDto[]>([])
  const isLoading = ref(false)

  async function fetchNews(query?: string) {
    isLoading.value = true
    await delay(400)
    const params = query?.trim() ? { search: query.trim() } : {}
    newsList.value = []

    try {
      const response = await axios.get(newsEndpoint, {
        params,
      })
      newsList.value = response.data
    } catch (error) {
      newsList.value = []
      console.error('Ошибка при загрузке новостей: ', error)
    } finally {
      isLoading.value = false
    }
  }

  function delay(ms: number) {
    return new Promise((resolve) => setTimeout(resolve, ms))
  }

  return { newsList, isLoading, fetchNews }
}
