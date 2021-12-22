import { useRef, useState } from "react";
import { Redirect } from "react-router-dom";
import { fetchCategoryList, fetchClientList, fetchOrders, fetchProductList, postClient, postOrderItem, postPaymentDetail, postOrder } from "../../api";
import { Link } from "react-router-dom";
import {
	Alert,
	Image,
	Button,
	Box,
	Text,
	Modal,
	ModalOverlay,
	ModalContent,
	ModalHeader,
	ModalFooter,
	ModalBody,
	ModalCloseButton,
	useDisclosure,
	FormControl,
	FormLabel,
	Textarea,
	Radio,
	Stack,
	RadioGroup
} from "@chakra-ui/react";
import { useBasket } from "../../contexts/BasketContext";
import { useQuery } from "react-query";
	

function Basket() {
	const [addressLine, setAddressLine] = useState("");
	const [firstname, setFirstName] = useState("");
	const [lastname, setLastName] = useState("");
	const [phone, setPhone] = useState("");
	const [city, setCity] = useState("");
	const [postalCode, setPostalCode] = useState("");
	const [paymentType, setPaymentType] = useState(null);

	const { isOpen: isClientInfoOpen, onOpen: onClientInfoOpen, onClose: onClientInfoClose } = useDisclosure();
	const { isOpen: isPaymentOpen, onOpen: onPaymentOpen, onClose: onPaymentClose } = useDisclosure();
	const { isOpen: isOverviewOpen, onOpen: onOverviewOpen, onClose: onOverviewClose } = useDisclosure();
	
	const initialRef = useRef();

	const { items, removeFromBasket, emptyBasket } = useBasket();
	const total = items.reduce((acc, obj) => acc + obj.price, 0);

	// multiple queries
	const { data:orderDetails, error:od_error, loading: od_load } = useQuery("od",fetchOrders);
	const { data:clients, error:c_error, loading: c_load } = useQuery("c", fetchClientList)

	if (od_load === "loading") return "Loading...";
	
	const handleClientSubmitForm = async () => {
		if (firstname && lastname && phone && addressLine && postalCode) {
			onClientInfoClose();
			onPaymentOpen();
		}
		else {
			return "Not valid"
		}
	};
	
	const handleOrderItemInput = (orderItemsInput) => {
		orderItemsInput.map((od) => (postOrderItem(od)))
	}
	
	const handlePaymentSubmitForm = async () => {
		if (paymentType === null) {
			return "Not valid!"
		}

		const itemIds = items.map((item) => item.id);
		// client fill
		const clientInput = {
			firstname,
			lastname,
			phone,
			city,
			postalCode,
			addressLine		
		}
		// order details fill
		const orderDetailsInput = {
			totalPrice: total*(118/100),
			createDate : new Date().toISOString(),
			clientId : clients.at(-1).id+1
		}
		// order items fill
		const orderItemsInput = itemIds.map((id) => (
			{
			 quantity: 1,
			 createDate: new Date().toISOString(),
			 productId: id,
			 orderDetailId: orderDetails.at(-1).id+1,
			}
		))
	
		// payment details fill
		const paymentDetailsInput = {
			amount : total*(118/100),
			type : paymentType,
			provider: "abc",
			orderDetailId : orderDetails.at(-1).id+1
		}

		await postClient(clientInput)
		await postOrder(orderDetailsInput);
		await handleOrderItemInput(orderItemsInput)
		await postPaymentDetail(paymentDetailsInput)
		
		onPaymentClose();
		onOverviewOpen();
	}
	function handleOverviewForm() {
		onOverviewClose();
		emptyBasket();
	}

	return (
		<Box p="5">
			{items.length < 1 && (
				<Alert status="warning">You have not any items in your basket.</Alert>
			)}

			{items.length > 0 && (
				<>
					<ul style={{ listStyleType: "decimal" }}>
						{items.map((item) => (
							<li key={item.id} style={{ marginBottom: 15 }}>
								<Link to={`/product/${item.id}`}>
									<Text fontSize="18">
										{item.title} - {item.price} TL
									</Text>
									<Image
										htmlWidth={100}
										loading="lazy"
										src={item.pictureUrl}
										alt="basket item"
									/>
								</Link>
								<Button
									mt="2"
									size="sm"
									colorScheme="pink"
									onClick={() => removeFromBasket(item.id)}
								>
									Remove from basket
								</Button>
							</li>
						))}
					</ul>
					<Box mt="10">
						<Text fontSize="22">Total: {total} TL</Text>
					</Box>

					<Box mt="10">
						<Text fontSize="22">Total (Tax Included): {total*(118/100)} TL</Text>
					</Box>

					<Button mt="2" size="sm" colorScheme="green" onClick={onClientInfoOpen}>
						Complete the Order
					</Button>
					{/* Client */}
					<Modal initialFocusRef={initialRef} isOpen={isClientInfoOpen} onClose={onClientInfoClose}>
						<ModalOverlay />
						<ModalContent>
							<ModalHeader>Order</ModalHeader>
							<ModalCloseButton />
							<ModalBody pb={6}>
								<FormControl isRequired>
									<FormLabel>First Name</FormLabel>
									<Textarea
										ref={initialRef}
										placeholder="First Name"
										value={firstname}
										onChange={(e) => setFirstName(e.target.value)}
									/>
								</FormControl>
							</ModalBody>
							<ModalBody pb={6}>
								<FormControl isRequired>
									<FormLabel>Last Name</FormLabel>
									<Textarea
										ref={initialRef}
										placeholder="Last Name"
										value={lastname}
										onChange={(e) => setLastName(e.target.value)}
									/>
								</FormControl>
							</ModalBody>
							<ModalBody pb={6}>
								<FormControl required>
									<FormLabel>Phone</FormLabel>
									<Textarea
										ref={initialRef}
										placeholder="Phone"
										value={phone}
										onChange={(e) => setPhone(e.target.value)} 
										required
									/>
								</FormControl>
							</ModalBody>
							<ModalBody pb={6}>
								<FormControl isRequired>
									<FormLabel>City</FormLabel>
									<Textarea
										ref={initialRef}
										placeholder="City"
										value={city}
										onChange={(e) => setCity(e.target.value)}
									/>
								</FormControl>
							</ModalBody>
							<ModalBody pb={6}>
								<FormControl isRequired>
									<FormLabel>Postal Code</FormLabel>
									<Textarea
										ref={initialRef}
										placeholder="Postal Code"
										value={postalCode}
										onChange={(e) => setPostalCode(e.target.value)} isRequired
									/>
								</FormControl>
							</ModalBody>
							<ModalBody pb={6}>
								<FormControl isRequired>
									<FormLabel>Address</FormLabel>
									<Textarea
										ref={initialRef}
										placeholder="Address"
										value={addressLine}
										onChange={(e) => setAddressLine(e.target.value)}
									/>
								</FormControl>
							</ModalBody>
							
							<ModalFooter>
									<Button colorScheme="blue" mr={3} onClick={handleClientSubmitForm}>
										Save
									</Button>
							
								<Button onClick={onClientInfoClose}>Cancel</Button>
							</ModalFooter>
						</ModalContent>
					</Modal>
					{/* Payment */}
					<Modal initialFocusRef={initialRef} isOpen={isPaymentOpen} onClose={onPaymentClose}>
						<ModalOverlay />
						<ModalContent>
							<ModalHeader>Payment</ModalHeader>
							<ModalCloseButton />
							<ModalBody pb={6}>
								<FormControl isRequired>
									<FormLabel>Select the type of the payment</FormLabel>
									<RadioGroup onChange={setPaymentType}>
										<Stack>
											<Radio size='lg' name='paymentType'  value='Credit Card' colorScheme='orange'>
												Credit Card
											</Radio>
											<Radio size='lg' name='paymentType' value='eCommerce Pay' colorScheme='orange'>
												eCommerce Pay
											</Radio>
											<Radio size='lg' name='paymentType' value='Paypal' colorScheme='orange'>
												Paypal
											</Radio>
										</Stack>
									</RadioGroup>

								</FormControl>
							</ModalBody>
							<ModalFooter>
									<Button colorScheme="blue" mr={3} onClick={handlePaymentSubmitForm}>
										Approve the order
									</Button>
							
								<Button onClick={onPaymentClose}>Cancel</Button>
							</ModalFooter>
						</ModalContent>
					</Modal>
					{/* Overview Receipt */}
					
					<Modal initialFocusRef={initialRef} isOpen={isOverviewOpen} onClose={onOverviewClose}>
						<ModalOverlay />
						<ModalContent>
							<ModalHeader>Order Overview</ModalHeader>
							<ModalCloseButton />
							<ModalBody pb={6}>
								<Text mt={2} fontSize="xl" fontWeight="semibold" lineHeight="short">
									Order number: {orderDetails ? orderDetails.at(-1).id : "none"}
								</Text>
								<Text mt={2} fontSize="xl" fontWeight="semibold" lineHeight="short">
									Sum of order: {total*(118/100)}
								</Text>
								<Text mt={2} fontSize="xl" fontWeight="semibold" lineHeight="short">
									Address: {addressLine}
								</Text>
								<Text mt={2} fontSize="xl" fontWeight="semibold" lineHeight="short">
									Estimated delivery date: {new Date(Date.now() + 86400000 * 3).toLocaleDateString()}
								</Text>
							</ModalBody>
							<ModalFooter>
								<Button onClick={handleOverviewForm}>Close</Button>
							</ModalFooter>
						</ModalContent>
					</Modal>
				</>
			)}
		</Box>
	);
}

export default Basket;
