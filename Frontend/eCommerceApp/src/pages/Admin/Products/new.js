import React from "react";
import { useQuery } from "react-query";
import { fetchCategoryList, postProduct, postCategory, fetchVendorList } from "../../../api";
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
import validationSchema from "./validations";

function NewProduct() {
	const queryClient = useQueryClient();
	const newProductMutation = useMutation(postProduct, {
		onSuccess: () => queryClient.invalidateQueries("products"),
	});
	const { status:cLoading, data:categories, error:cat_error, loading: cat_load } = useQuery("cats",fetchCategoryList);
	const { status:vLoading, data:vendors, error:vendors_error, loading: vendors_load } = useQuery("vendors",fetchVendorList);
	
	if (vLoading === "loading") return "Loading...";
	if (cLoading === "loading") return "Loading...";
	
	const handleSubmit = async (values, bag) => {
		console.log(values);
		message.loading({ content: "Loading...", key: "product_update" });

		const newValues = {
			...values
		};
		try {
			await postProduct(newValues)
		} catch (error) {
			message.error("The product does not created.");
		}
		newProductMutation.mutate(newValues, {
			onSuccess: () => {
				console.log("success");

				message.success({
					content: "The product successfully updated",
					key: "product_update",
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
					title: "Test",
					price: "100",
					pictureUrl: "",
					categoryId: "1",
					vendorId: "2"
				}}
				validationSchema={validationSchema}
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
										<FormLabel>Title</FormLabel>
										<Input
											name="title"
											onChange={handleChange}
											onBlur={handleBlur}
											value={values.title}
											disabled={isSubmitting}
											isInvalid={touched.title && errors.title}
										/>

										{touched.title && errors.title && (
											<Text color="red.500">{errors.title}</Text>
										)}
									</FormControl>
									
									<FormControl mt="4">
										<FormLabel>Price</FormLabel>
										<Input
											name="price"
											onChange={handleChange}
											onBlur={handleBlur}
											value={values.price}
											disabled={isSubmitting}
											isInvalid={touched.price && errors.price}
										/>
										{touched.price && errors.price && (
											<Text color="red.500">{errors.price}</Text>
										)}
									</FormControl>
									<FormControl mt="4">
										<FormLabel>Add a Photo</FormLabel>
										<Input
											name="pictureUrl"
											onChange={handleChange}
											onBlur={handleBlur}
											value={values.pictureUrl}
											disabled={isSubmitting}
										/>
									</FormControl>
									<FormControl mt="4">
										<FormLabel>Pick a Category</FormLabel>
										<select
										value={values.categoryId}
										>
											{categories.map((category) => (
												<option value={category.id} label={category.name} />
											))}
										</select>
									</FormControl>
									<FormControl mt="4">
										<FormLabel>Pick a Vendor</FormLabel>
										<select
										value={values.vendorId}
										>
											{vendors.map((vendor) => (
												<option value={vendor.id} label={vendor.vendorName} />
											))}
										</select>
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
