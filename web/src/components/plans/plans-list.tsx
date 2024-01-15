import React, { useEffect, useState } from "react"
import { Box } from "@mui/material"
import AddIcon from '@mui/icons-material/Add';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import ArrowForwardIcon from '@mui/icons-material/ArrowForward';
import ListIcon from '@mui/icons-material/List';

import "./plans.scss"
import PlansListElement from "./meal";
import appTheme from "../theme";
import moment from "moment";
import CloseIcon from '@mui/icons-material/Close';
import { RecipeListMode } from "../../enums/recipe";
import Meal from "./meal";
import RecipeList from "../recipes/recipe-list/recipe-list";
import { MealGetResponse, MealPostRequest, MealsService, OpenAPI, PlanGetResponse, PlansService, RecipeGetResponse } from "@dietly/sdk";
import { DefaultDateFormat, DefaultTimeFormat } from "../../constants/time";

const DAYS: Array<string> = [
    "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"
]

const YESTERDAY: string = "Yesterday: ";
const TODAY: string = "Today: ";
const TOMORROW: string = "Tomorrow: ";

const DateDiffInDays = (date1: Date, date2: Date): number => {
    return moment(date2).startOf('day').diff(moment(date1).startOf('day'), 'days')
}

const GetDayOfWeek = (date: Date): number => {
    return (date.getDay() - 1 + DAYS.length) % DAYS.length
}
const PlansList = () => {

    const [consumedCalories, setConsumedCalories] = useState<number | undefined>(0)
    const [totalCalories, setTotalCalories] = useState<number | undefined>(0)
    const [meals, setMeals] = useState<Array<MealGetResponse> | undefined | null>(null)
    const [selectedDay, setSelectedDay] = useState<Date>(new Date)

    const [dayOfWeek, setDayOfWeek] = useState<number>()
    const [dayPrefix, setDayPrefix] = useState<string>("")
    const [forceRefreshMeals, setForceRefreshMeals] = useState<boolean>(false)

    const [recipePanelVisible, setRecipePanelVisible] = useState<boolean>(false)

    const setDates = (operation: number) => {
        const newDay = moment(selectedDay).add(operation, 'days').toDate();
        const newDayOfWeek = GetDayOfWeek(newDay)

        setSelectedDay(newDay)
        setDayOfWeek(newDayOfWeek)
        calcDayPrefix(newDay)
    }

    const calcDayPrefix = (newDay: Date) => {
        const dayFromTodayDiff = DateDiffInDays(new Date, newDay);
        if (dayFromTodayDiff === -1) {
            setDayPrefix(YESTERDAY)
        } else if (dayFromTodayDiff === 0) {
            setDayPrefix(TODAY)
        } else if (dayFromTodayDiff === 1) {
            setDayPrefix(TOMORROW)
        } else {
            setDayPrefix("")
        }
    }

    const addNewMeal = (hour: Date, recipe: RecipeGetResponse) => {
        setRecipePanelVisible(false)
        const mealData: MealPostRequest = {
            recipeId: recipe.id,
            date: `${moment(selectedDay).format(DefaultDateFormat)}T${moment(hour).format(DefaultTimeFormat)}`
        }

        MealsService.addMeal(mealData)
            .then(() => {
                getPlanForDay(selectedDay)
            })
            .catch(() => { })
    }

    const getPlanForDay = (day: Date) => {
        if (!OpenAPI.TOKEN)
            return;

        PlansService.getPlan(moment(day).format(DefaultDateFormat))
            .then((response: PlanGetResponse) => {
                setConsumedCalories(response.consumedCalories)
                setTotalCalories(response.totalCalories)
                setMeals(response.meals?.sort((a, b) => {
                    return moment(a.date).diff(moment(b.date))
                }))

            })
            .catch((err: any) => {
                setConsumedCalories(0)
                setTotalCalories(0)
                setMeals(null)
            })
    }
    const handleForceRefresh = () => {
        setForceRefreshMeals(!forceRefreshMeals)
    }

    useEffect(() => {
        getPlanForDay(selectedDay)
    }, [selectedDay, forceRefreshMeals])

    useEffect(() => {
        const today: Date = new Date
        calcDayPrefix(today)
        setDayOfWeek(GetDayOfWeek(today))
    }, [])

    return (
        <>
            <Box sx={{ pt: 3, display: 'flex', justifyContent: "space-between" }}>

                <div className="plans-list-navigation">
                    <div className="plans-list-navigation-arrows">
                        <div className="button-std nav-button nav-button-left" onClick={() => setDates(-1)}>
                            <ArrowBackIcon htmlColor="black" fontSize="small" />
                        </div>
                        <div className="button-std nav-button nav-button-right" onClick={() => setDates(1)}>
                            <ArrowForwardIcon htmlColor="black" fontSize="small" />
                        </div>
                    </div>

                    <div className="plans-list-navigation-day">{dayPrefix}</div>
                    <div className="plans-list-navigation-date">{(moment(selectedDay)).format('dddd, DD MMM')}</div>
                </div>

                <div className="plans-list-buttons">
                    <div className="plans-list-calories">{consumedCalories} / {totalCalories}kcal.</div>

                    <div className="button-std add-button" onClick={() => setRecipePanelVisible(true)}>
                        <AddIcon />
                    </div>
                </div>

            </Box>

            <Box sx={{ pt: 3 }}>
                <div className="plans-list-week-days">
                    {DAYS.map((day: string, index: number) => {
                        return (
                            <div
                                className="plans-list-day"
                                key={"day" + index}
                                style={{ backgroundColor: index == dayOfWeek ? appTheme.palette.primary.main : "transparent" }}>{day}</div>)
                    })}
                </div>
            </Box>
            <Box sx={{ pt: 3 }}>
                {meals?.map((meal: MealGetResponse, index: number) => {
                    return <Meal onReload={handleForceRefresh} data={meal} key={"meal" + index} />
                })}

            </Box>

            {recipePanelVisible &&
                <div className="plans-list-add-container">
                    <div className="plans-list-recipe-list-wrapper">
                        <CloseIcon className="plans-list-recipe-list-close" onClick={() => setRecipePanelVisible(false)} />
                        <RecipeList mode={RecipeListMode.Select} onAccept={addNewMeal} />
                    </div>
                </div>
            }

        </>
    )
}

export default PlansList
