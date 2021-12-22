import React from "react";

import { useParams } from "react-router-dom";
import { fetchCategory, updateCategory } from "../../../api";
import { useQuery } from "react-query";
import {
	Text,
	Box,
	FormControl,
	FormLabel,
	Input,
	Textarea,
	Button,
} from "@chakra-ui/react";
import { message } from "antd";

import { Formik, FieldArray } from "formik";

function CategoryDetail() {
	const { category_id } = useParams();

	const { isLoading, isError, data, error } = useQuery(
		["admin:category", category_id],
		() => fetchCategory(category_id)
	);

	if (isLoading) {
		return <div>Loading...</div>;
	}

	if (isError) {
		return <div>Error {error.message}</div>;
	}

	const handleSubmit = async (values, bag) => {
		console.log(values);
		console.log("submitted");
		message.loading({ content: "Loading...", key: "category_update" });

		try {
			await updateCategory(values, category_id);

			message.success({
				content: "The product successfully updated",
				key: "product_update",
				duration: 2,
			});
		} catch (e) {
			message.error("The product does not updated.");
		}
	};
	return (
		<div>
			<Text fontSize="2xl">Edit</Text>

			<Formik
				initialValues={{
					name: data.name,
					
				}}
				onSubmit={handleSubmit}
			>
				{({
					handleSubmit,
					errors,
					touched,
					handleChange,
					handleBlur,
					values,
					isSubmitting,
				}) => (
					<>
						<Box>
							<Box my="5" textAlign="left">
								<form onSubmit={handleSubmit}>
									<FormControl>
										<FormLabel>Category Name</FormLabel>
										<Input
											name="name"
											onChange={handleChange}
											onBlur={handleBlur}
											value={values.name}
											disabled={isSubmitting}
											isInvalid={touched.name && errors.name}
										/>

										{touched.name && errors.name && (
											<Text color="red.500">{errors.name}</Text>
										)}
									</FormControl>
					

									<Button
										mt={4}
										width="full"
										type="submit"
										isLoading={isSubmitting}
									>
										Update
									</Button>
								</form>
							</Box>
						</Box>
					</>
				)}
			</Formik>
		</div>
	);
}

export default CategoryDetail;

