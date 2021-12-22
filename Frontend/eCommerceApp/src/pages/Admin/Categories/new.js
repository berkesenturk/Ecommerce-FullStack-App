import React from "react";
import { useQuery } from "react-query";
import { postCategory } from "../../../api";
import { useMutation, useQueryClient } from "react-query";

import {
	Text,
	Box,
	FormControl,
	FormLabel,
	Input,
	Textarea,
	
	Button
} from "@chakra-ui/react";
import { message } from "antd";
import { Formik, FieldArray } from "formik";

function NewProduct() {
	const queryClient = useQueryClient();
	const newProductMutation = useMutation(postCategory, {
		onSuccess: () => queryClient.invalidateQueries("categories"),
	});
	
	const handleSubmit = async (values, bag) => {
		console.log(values);
		message.loading({ content: "Loading...", key: "category_update" });

		const newValues = {
			...values
		};
		try {
			await postCategory(newValues)
		} catch (error) {
			message.error("The category does not created.");
		}
		newProductMutation.mutate(newValues, {
			onSuccess: () => {
				console.log("success");

				message.success({
					content: "The category successfully updated",
					key: "category_update",
					duration: 2,
				});
			},
		});

	};
	return (
		<div>
			<Text fontSize="2xl">New Product</Text>

			<Formik
				initialValues={{
					name: "Test"
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
										<FormLabel>name</FormLabel>
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
										Save
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

export default NewProduct;
