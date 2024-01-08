FROM node:18-alpine as builder
WORKDIR /app
COPY package*.json .
COPY turbo.json .
COPY web .
COPY lib .
RUN yarn install
RUN yarn build

FROM nginx:1.25.3-alpine
WORKDIR /usr/share/nginx/html
RUN rm -rf ./*
COPY --from=builder /app/build .
ENTRYPOINT ["nginx", "-g", "daemon off;"]
