import axios from "axios";
var querystring = require('querystring');


axios.interceptors.request.use(
	function (config) {
		const { origin } = new URL(config.url);

		return config;
	},
	function (error) {
		// Do something with request error
		return Promise.reject(error);
	}
);

export const fetchProductList = async () => {
	const { data } = await axios.get(
		`${process.env.REACT_APP_BASE_ENDPOINT}/Products`);
	return data;
};

export const fetchCategoryList = async () => {
	const { data } = await axios.get(
		`${process.env.REACT_APP_BASE_ENDPOINT}/Categorys`);
	return data;
};

export const fetchVendorList = async() => {
	const { data } = await axios.get(

		`${process.env.REACT_APP_BASE_ENDPOINT}/Vendors`);
	return data;
}

export const postVendor = async (input) => {
	const { data } = await axios.post(
		`${process.env.REACT_APP_BASE_ENDPOINT}/Vendors/`,
		input
	);

	return data;
};

export const fetchClientList = async() => {
	const { data } = await axios.get(

		`${process.env.REACT_APP_BASE_ENDPOINT}/Clients`);
	return data;
}

export const postClient = async (input) => {
	const { data } = await axios.post(
		`${process.env.REACT_APP_BASE_ENDPOINT}/Clients/`,
		input
	);

	return data;
};

export const fetchCategory = async (id) => {
	const { data } = await axios.get(
		`${process.env.REACT_APP_BASE_ENDPOINT}/Categorys/${id}`
	);

	return data;
};

export const postCategory = async (input) => {
	const { data } = await axios.post(
		`${process.env.REACT_APP_BASE_ENDPOINT}/Categorys/`,
		input,{
			headers: {"Content-Type": "application/json"}
		 }
	).catch(error =>{})
	

	return data;
};

export const deleteCategory = async (category_id) => {
	const { data } = await axios.delete(
		`${process.env.REACT_APP_BASE_ENDPOINT}/Categorys/${category_id}`
	);

	return data;
};

export const updateCategory = async (input, category_id) => {
	const { data } = await axios.put(
		`${process.env.REACT_APP_BASE_ENDPOINT}/Categorys/${category_id}`,
		input,{
			headers: {"Content-Type": "application/json"}
		 }
	).catch(error =>{})

	return data;
};

export const fetchProduct = async (id) => {
	const { data } = await axios.get(
		`${process.env.REACT_APP_BASE_ENDPOINT}/Products/${id}`
	);

	return data;
};


export const postProduct = async (input) => {
	const { data } = await axios.post(
		`${process.env.REACT_APP_BASE_ENDPOINT}/Products/`,
		input,{
			headers: {"Content-Type": "application/json"}
		 }
	).catch(error =>{})

	return data;
};

export const postPaymentDetail = async (input) => {
	const { data } = await axios.post(
		`${process.env.REACT_APP_BASE_ENDPOINT}/PaymentDetails`,
		input
	);

	return data;
};


export const postOrderItem = async (input) => {
	const { data } = await axios.post(
		`${process.env.REACT_APP_BASE_ENDPOINT}/OrderItems`,
		input
	);

	return data;
};

export const postOrder = async (input) => {
	const { data } = await axios.post(
		`${process.env.REACT_APP_BASE_ENDPOINT}/OrderDetails`,
		input
	);

	return data;
};

export const fetchOrders = async () => {
	const { data } = await axios.get(
		`${process.env.REACT_APP_BASE_ENDPOINT}/OrderDetails`
	);

	return data;
};

export const deleteProduct = async (product_id) => {
	const { data } = await axios.delete(
		`${process.env.REACT_APP_BASE_ENDPOINT}/Products/${product_id}`
	);

	return data;
};



export const updateProduct = async (input, product_id) => {
	const { data } = await axios.put(
		`${process.env.REACT_APP_BASE_ENDPOINT}/Products/${product_id}`,
		input
	);

	return data;
};
