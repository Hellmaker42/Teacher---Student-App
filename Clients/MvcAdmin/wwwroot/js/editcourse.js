// var jsVideoYes = document.querySelector('.videoYes');
// var jsVideoNo = document.querySelector('.videoNo');
var checkbox = document.getElementById('isVideoYes');
// const jsDays = document.querySelector('.days');
// const jsHours = document.querySelector('.hours');
// const jsMinutes = document.querySelector('.minutes');
// const jsTestElement = document.querySelector('.testar');
const jsDivNotVideo = document.querySelector('.divNotVideo');
const jsDivIsVideo = document.querySelector('.divIsVideo');

// if (document.querySelector('input[name="isVideo"]')) {
//   document.querySelectorAll('input[name="isVideo"]').forEach((elem) => {
//     elem.addEventListener("change", checkIsVideo);
// });
// }
checkbox.addEventListener('change', checkIsVideo);

// jsIsvideo.addEventListener('change', checkIsVideo);

// console.log(jsIsvideo.value);
console.log(checkbox.checked);
checkIsVideo();
// function checkIsVideo() {
//   if (jsIsvideo.value) {
//     // jsIsvideo.checked = false;
//     jsDivNotVideo.style.display = 'none';
//     jsDivIsVideo.style.display = 'inline';
//     console.log('checked');
//   } else {
//     // jsIsvideo.checked = true;
//     jsDivIsVideo.style.display = 'none';
//     jsDivNotVideo.style.display = 'inline';
//     console.log('not checked');
//   }
// }

function checkIsVideo() {
  if (checkbox.checked) {
    jsDivNotVideo.style.display = 'none';
    jsDivIsVideo.style.display = 'inline';
    console.log("checked");
  } else {
    jsDivIsVideo.style.display = 'none';
    jsDivNotVideo.style.display = 'inline';
    console.log("not checked");
  }
}
