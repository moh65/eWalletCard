# Specifies where to get the base image (Node v12 in our case) and creates a new container for it
FROM node:14-alpine as build-step

# Set working directory. Paths will be relative this WORKDIR.
WORKDIR /usr/src/pwd

# Install dependencies
COPY package*.json ./
RUN npm install
ARG API_URL API_USERNAME API_PASSWORD 
ENV API_URL=$API_URL API_USERNAME=$API_USERNAME API_PASSWORD=$API_PASSWORD 
# Copy source files from host computer to the container
COPY . .
# Build the app
#RUN npm run build
# Specify port app runs on
EXPOSE 3002

# Run the app
CMD [ "npm", "start" ]
# docker run -d -it  -p 5020:5022 wallet-pwa:latest