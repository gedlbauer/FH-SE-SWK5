const btnFetchCustomer: HTMLInputElement = <HTMLInputElement>document.getElementById("btnFetchCustomer");
const txtCustomerId: HTMLInputElement = <HTMLInputElement>document.getElementById("txtCustomerId");
const txtName: HTMLInputElement = <HTMLInputElement>document.getElementById("txtName");
const txtZipCode: HTMLInputElement = <HTMLInputElement>document.getElementById("txtZipCode");
const txtCity: HTMLInputElement = <HTMLInputElement>document.getElementById("txtCity");
const txtRating: HTMLInputElement = <HTMLInputElement>document.getElementById("txtRating")

function displayCustomer(customer: Customer) {
    txtName.value = customer.name;
    txtZipCode.value = customer.zipCode;
    txtCity.value = customer.city;
    txtRating.value = customer.rating;
}

function setValidationError(inputElement: HTMLInputElement) {
    inputElement.classList.add("is-invalid");
}

function resetValidationError(inputElement: HTMLInputElement) {
    inputElement.classList.remove("is-invalid");
}

function onFetchButtonClicked() {
    let customerId = txtCustomerId.value;
    resetValidationError(txtCustomerId);

    fetchCustomerByIdAsync(customerId)
        .then((customer: Customer) => displayCustomer(customer))
        .catch(_ => setValidationError(txtCustomerId));

}

window.onload = () => btnFetchCustomer.onclick = onFetchButtonClicked
