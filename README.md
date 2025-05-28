# ADITUS Code Challenge API

This API provides endpoints for managing events and hardware reservations for the ADITUS Code Challenge platform.

## Overview
- Retrieve a list of events
- Get details and statistics for a specific event
- Reserve hardware for an event
- Retrieve hardware reservation details

---

## Endpoints

### 1. `GET /events`
- **Description:** Retrieves a list of all events.
- **Request Body:** None
- **Response:** Array of `Event` objects.

### 2. `GET /events/{id}`
- **Description:** Retrieves details for a specific event, including statistics.
- **Path Parameter:** 
  - `id` (Guid, required): The unique identifier of the event.
- **Response:** An `Event` object with statistics.

### 3. `POST /hardwarereservation`
- **Description:** Reserves hardware for a specific event.
- **Request Body:** 
  - `HardwareReservationRequest` object:
    - `eventId` (Guid, required): The event for which hardware is reserved.
    - `components` (Dictionary<HardwareComponent, int>, required): The hardware components and their quantities.
- **Response:** A `HardwareReservation` object with reservation details and status.

### 4. `GET /hardwarereservation/{id}`
- **Description:** Retrieves the hardware reservation for a specific event.
- **Path Parameter:** 
  - `id` (Guid, required): The unique identifier of the event.
- **Response:** A `HardwareReservation` object.

---

## Data Models

### Event
- `id`: Guid
- `year`: int
- `name`: string
- `type`: EventType (OnSite, Online, Hybrid)
- `startDate`: DateTime
- `endDate`: DateTime
- `statistics`: EventStatistics

### HardwareReservationRequest
- `eventId`: Guid
- `components`: Dictionary<HardwareComponent, int>

### HardwareReservation
- `eventId`: Guid
- `reservedComponents`: Dictionary<HardwareComponent, int>
- `status`: ReservationStatus (None, PendingApproval, Approved, Rejected)

---

## Usage
- All endpoints accept and return JSON.
- For more details and interactive documentation, use the built-in Swagger UI at `/swagger` when running the API locally.

---

*Generated on 2025-05-29*
