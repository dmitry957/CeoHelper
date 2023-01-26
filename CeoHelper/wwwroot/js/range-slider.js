

// range slider in service details
var sliderEl1 = document.querySelector('.first [data-rangeslider]')
var sliderEl2 = document.querySelector('.second [data-rangeslider]')
var sliderEl3 = document.querySelector('.third [data-rangeslider]')
var sliderEl4 = document.querySelector('.fourth [data-rangeslider]')
var sliderEl5 = document.querySelector('.fifth [data-rangeslider]')

var firstOutput;
var secondOutput;
var thirdOutput;

sliderEl1.addEventListener('input', (ev) => {
  document.querySelector('.lc-amount .output').innerHTML = ev.target.value;
  firstOutput = ev.target.value;
  monthlyAmount();
})
sliderEl2.addEventListener('input', (ev) => {
  document.querySelector('.lc-term .output').innerHTML = ev.target.value;
  secondOutput = ev.target.value;
  monthlyAmount();
})

sliderEl3.addEventListener('input', (ev) => {
  document.querySelector('.lc-interest .output').innerHTML = ev.target.value;
  thirdOutput = ev.target.value;
  monthlyAmount();
})

sliderEl4.addEventListener('input', (ev) => {
  document.querySelector('.lc-amount .output').innerHTML = ev.target.value;
  firstOutput = ev.target.value;
  monthlyAmount();
})

sliderEl5.addEventListener('input', (ev) => {
  document.querySelector('.lc-term .output').innerHTML = ev.target.value;
  secondOutput = ev.target.value;
  monthlyAmount();
})

function monthlyAmount() {
  document.querySelector('.lc-monthly .output').innerHTML = Math.ceil((firstOutput + secondOutput / 12) * thirdOutput / 100);
}
// rangesliderJs.create(sliderEl1, {min: 0, max: 5, step: 0.5})
rangesliderJs.create(sliderEl1)
rangesliderJs.create(sliderEl2)
rangesliderJs.create(sliderEl3)
rangesliderJs.create(sliderEl4)
rangesliderJs.create(sliderEl5)