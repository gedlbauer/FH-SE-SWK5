const ORDER_MANAGEMENT_BASE_URI = "https://localhost:5001";

interface Customer {
    id: string;
    name: string;
    zipCode: string;
    city: string;
    rating: string;
}

async function fetchCustomerByIdAsync(customerId: string): Promise<Customer> {
    if (!customerId) {
        throw new Error('ID must not be empty');
    }

    const response = await fetch(`${ORDER_MANAGEMENT_BASE_URI}/api/customers/${customerId}`, {
        method: 'GET',
        headers: { 'Accept': 'application/json' }
    });

    if (response.status != 200) {
        throw new Error(`Failed with status code ${response.status}`);
    }
    return response.json();
}