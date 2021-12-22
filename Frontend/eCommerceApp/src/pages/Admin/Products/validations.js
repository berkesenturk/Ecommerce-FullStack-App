import * as yup from "yup";

const newProductScheme = yup.object().shape({
	title: yup.string().required(),
	price: yup.string().required(),
});

export default newProductScheme;
