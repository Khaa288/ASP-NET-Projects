"use strict";
import * as CRUD from '../../View/script.js'
const saveButton = document.querySelector("#btnSave")
const titleInput = document.querySelector("#title")
const descriptionInput = document.querySelector("#description")
const deleteButton = document.querySelector("#btnDelete")

// Trigger every action to all client hubs
async function HubTrigger(event) {
    await connection.invoke("DisplayNotes").catch(function(err) {
        return console.error(err.toString());
    });
    event.preventDefault();
}

// Create Connection with builder
var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:5078/noteHub", {
    skipNegotiation: true,
    transport: signalR.HttpTransportType.WebSockets
}).build();

// Start connection
connection.start()
    .catch(function(err) {
        return console.error(err.toString());
    });

// recieve response tu server roi display ra 
await connection.on("Update_Notes", function() {
    CRUD.getAllNotes();
});

// Cac event listener tren button
saveButton.addEventListener("click", async function() {
    // Action (add note)
    const id = saveButton.dataset.id;
    if (id != null) {
        await CRUD.updateNote(saveButton.dataset.id, titleInput.value, descriptionInput.value);
    } else {
        await CRUD.addNote(titleInput.value, descriptionInput.value)
    }

    // Trigger action to all clients
    HubTrigger();
});

deleteButton.addEventListener('click', async function() {
    // Action (delete)
    const id = deleteButton.dataset.id;
    await CRUD.deleteNote(id);

    // Trigger action to all clients
    HubTrigger();
})