const saveButton = document.querySelector("#btnSave")
const titleInput = document.querySelector("#title")
const descriptionInput = document.querySelector("#description")
const notesContainer = document.querySelector("#notes_container")
const deleteButton = document.querySelector("#btnDelete")

async function resetForm() {
    titleInput.value = '';
    descriptionInput.value = '';
    deleteButton.classList.add("hidden");
    saveButton.removeAttribute("data-id");
}

// gọi API xem all notes
async function getAllNotes() {
    console.log('tao duoc goi ne')
    await fetch('http://localhost:5078/api/notes')
        .then(data => data.json())
        .then(response => displayNotes(response))
        .catch(error => console.log(error));
}

// gọi API xem note by id (sau khi click vào mỗi note trên UI)
function populateForm(note) {
    titleInput.value = note.title;
    descriptionInput.value = note.description;
    deleteButton.classList.remove("hidden");
    deleteButton.setAttribute('data-id', note.id);
    saveButton.setAttribute('data-id', note.id);
}

async function getNoteById(id) {
    await fetch(`http://localhost:5078/api/notes/${id}`)
        .then(data => data.json())
        .then(response => populateForm(response));
}

// gọi API thêm note
async function addNote(title, description) {
    const body = {
        title: title,
        description: description,
        isVisible: true
    }

    await fetch('http://localhost:5078/api/notes', {
            method: 'POST',
            body: JSON.stringify(body),
            headers: {
                "content-type": "application/json"
            }
        })
        .then(data => data.json())
        .then(response => {
            // xóa 2 fields input
            titleInput.value = '';
            descriptionInput.value = '';
            getAllNotes();
        });
}

// gọi API xóa note
async function deleteNote(id) {
    await fetch(`http://localhost:5078/api/notes/${id}`, {
            method: 'DELETE',
            headers: {
                "content-type": "application/json"
            }
        })
        .then(data => data.json())
        .then(response => {
            titleInput.value = '';
            descriptionInput.value = '';
            resetForm();
            getAllNotes();
        })
}

// gọi API cập nhật 1 note
async function updateNote(id, title, description) {
    const body = {
        id: id,
        title: title,
        description: description,
        isVisible: true
    }

    await fetch(`http://localhost:5078/api/notes/${id}`, {
            method: 'PUT',
            body: JSON.stringify(body),
            headers: {
                "content-type": "application/json"
            }
        })
        .then(data => data.json())
        .then(response => {
            // xóa 2 fields input
            titleInput.value = '';
            descriptionInput.value = '';
            resetForm();
            getAllNotes();
        });
}


async function displayNotes(notes) {
    let allNotes = '';
    await notes.forEach(note => {
        const noteElement = `
            <div class="note" data-id="${note.id}">
                <h3>${note.title}</h3>
                <p>${note.description}</p>
            </div>
        `;
        allNotes += noteElement;
    });
    notesContainer.innerHTML = allNotes;

    // hiển thị guid mỗi khi click vào 1 note
    document.querySelectorAll('.note').forEach(note => {
        note.addEventListener('click', function() {
            getNoteById(note.dataset.id);
        });
    });
}


// Event Listeners
// saveButton.addEventListener('click', function() {
//     const id = saveButton.dataset.id;
//     if (id != null) {
//         updateNote(saveButton.dataset.id, titleInput.value, descriptionInput.value);
//     } else {
//         addNote(titleInput.value, descriptionInput.value)
//     }
// })

// deleteButton.addEventListener('click', function() {
//     const id = deleteButton.dataset.id;
//     deleteNote(id);
// })

window.addEventListener('DOMContentLoaded', function() {
    getAllNotes();
})

export { getAllNotes, getNoteById, addNote, updateNote, deleteNote };