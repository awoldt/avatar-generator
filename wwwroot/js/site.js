const baseAvatarBtns = document.getElementsByClassName("btn-check");
const avatarAdjSelect = document.getElementById("avatar_adj_select");
const pictureFinishSelect = document.getElementById("avatar_finish_select");
const formSubmitBtn = document.getElementById("submit_form_btn");
const FORM = document.getElementById("form");

Array.from(baseAvatarBtns).forEach((e) => {
  //once user clicks on any base avatar radio input
  //rest of form disabled attributes become false (can select values now)
  e.addEventListener("click", () => {
    if ((avatarAdjSelect.disabled = true)) {
      avatarAdjSelect.disabled = false;
    }
    if ((pictureFinishSelect.disabled = true)) {
      pictureFinishSelect.disabled = false;
    }
    if ((formSubmitBtn.disabled = true)) {
      formSubmitBtn.disabled = false;
    }
  });
});


FORM.addEventListener("submit", () => {
  const loading = document.createElement("div");
  loading.setAttribute("class", "spinner-border text-primary");
  loading.setAttribute("role", "status");

  formSubmitBtn.replaceWith(loading);
})
