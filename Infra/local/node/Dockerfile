FROM node:18-alpine as build
WORKDIR /app
COPY package.json .
COPY turbo.json .
COPY web ./web
COPY lib ./lib
COPY ./infra/local/node/nginx.conf .
RUN npm install -g npm@latest
RUN npm install
RUN npm run build

FROM nginx:1.25.3-alpine
COPY --from=build /app/nginx.conf /etc/nginx/conf.d/default.conf
WORKDIR /usr/share/nginx/html
RUN rm -rf ./*
COPY --from=build /app/web/build .
EXPOSE 80
ENTRYPOINT ["nginx", "-g", "daemon off;"]
