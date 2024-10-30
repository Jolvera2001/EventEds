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
const createSquareBounds = (centerLng: number, centerLat: number, sideLength: number) => {
  const degreeLength = sideLength / 111
  
  return {
    north: centerLat + (degreeLength / 2),
    south: centerLat - (degreeLength / 2),
    east: centerLng + (degreeLength / 2),
    west: centerLng - (degreeLength / 2)
  };
};

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
  const center = [-97.753, 30.229]
  const boundaryBox = createSquareBounds(center[0], center[1], 2);
  
  map = new mapboxgl.Map({
    container: mapContainer.value,
    style: 'mapbox://styles/mapbox/streets-v11',
    center: [-97.753, 30.229],
    zoom: 16,
    maxBounds: [
        [boundaryBox.west, boundaryBox.south],
        [boundaryBox.east, boundaryBox.north],
    ]
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

const handleCancel = () => {
  eventData.title = ''
  eventData.description = ''
  eventData.datetime = null
  eventData.location = {
    type: 'Point',
    coordinates: []
  }
}

const handleSubmit = async () => {
  try {
    isLoading.value = true
    console.log("Submitting...", eventData)
    console.log("This should submit to the event api")
  } catch (e) {
    console.error(e)
  } finally {
    eventData.title = ''
    eventData.description = ''
    eventData.datetime = null
    eventData.location = {
      type: 'Point',
      coordinates: []
    }
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
        <div class="flex flex-row justify-between gap-4">
          <Button 
              @click="handleSubmit"
              :loading="isLoading"
              class="flex-1 mt-2"
          >
            Create
          </Button>
          <Button
              @click="handleCancel"
              class="flex-1 mt-2"
          >
            cancel
          </Button>
        </div>
      </div>
    </template>
  </Card>
</template>
