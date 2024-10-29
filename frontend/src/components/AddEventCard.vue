<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue';
import mapboxgl from 'mapbox-gl';
import 'mapbox-gl/dist/mapbox-gl.css';

type GeoPoint = {
  type: 'Point',
  coordinates: number[];
}

interface EventData {
  title: string;
  description: string;
  datetime: Date | null;
  location: GeoPoint;
}

const MAPBOX_TOKEN = import.meta.env.VITE_MAPBOX_TOKEN
const mapContainer = ref(null)
let map = null
let marker = null

// form state
const eventData = reactive<EventData>({
  title: '',
  description: '',
  datetime: null,
  location: {
    type: 'Point',
    coordinates: []
  }
})

const isLoading = ref(false);

// init map
onMounted(() => {
  if (!mapContainer.value) return;
  
  
  mapboxgl.accessToken = MAPBOX_TOKEN
  
  map = new mapboxgl.Map({
    container: mapContainer.value,
    style: 'mapbox://styles/mapbox/streets-v11',
    center: [-74.5, 40],
    zoom: 9
  })
  
  // add click handler
  map.on('click', (e) => {
    const { lng, lat } = e.lngLat
    
    if (marker) {
      marker.remove()
    }
    marker = new mapboxgl.Marker()
        .setLngLat([lng, lat])
        .addTo(map!)
    
    // update form
    eventData.location = {
      type: 'Point',
      coordinates: [lng, lat]
    }
  })
})

const handleSubmit = async () => {
  try {
    isLoading.value = true
    console.log("Submitting...", eventData)
  } catch (e) {
    console.error(e)
  } finally {
    isLoading.value = false
  }
}
</script>

<template>
  <Card class="w-full max-w-md">
    <template #title>Add a New Event</template>
    <template #content>
      <div class="flex flex-col gap-2">
        <label for="eventTitle" >Title</label>
        <InputText 
            id="eventTitle"
            v-model="eventData.title"
        />
        <label for="eventDesc" >Description</label>
        <Textarea 
            id="eventDesc" 
            variant="filled"   
            cols="26"
            v-model="eventData.description"
        />
        <label for="eventDate" >Date and Time</label>
        <DatePicker 
            showTime 
            showIcon 
            hourFormat="12" 
            class="w-full"
            id="eventDate"
            v-model="eventData.datetime"
        />
        <!--map-->
        <div
            ref="mapContainer"
            class="w-full h-64 rounded-lg border"
        ></div>
        <div v-if="eventData.location.coordinates.length === 2" class="text-sm text-gray-600">
          Selected Location: {{ eventData.location.coordinates[0].toFixed(6) }},
          {{ eventData.location.coordinates[1].toFixed(6) }}
        </div>
        <Button 
            @click="handleSubmit"
            :loading="isLoading"
            class="mt-2"
        >
          Create
        </Button>
      </div>
    </template>
  </Card>
</template>
