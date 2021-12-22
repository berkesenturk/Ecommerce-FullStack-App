import * as yup from "yup";

const editScheme = yup.object().shape({
	title: yup.string().required(),
	price: yup.string().required()
});

export default editScheme;
