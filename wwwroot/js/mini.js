const fields = document.querySelectorAll(".form-control");
for (const field of fields) {
  field.addEventListener("blur", (event) => {
    const sel = event.target;
    if (sel.value) {
      sel.classList.add("filled");
    } else {
      sel.classList.remove("filled");
    }
  });
}
