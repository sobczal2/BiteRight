package com.sobczal2.biteright.util

import java.time.LocalDate

fun LocalDate.toEpochMillis(): Long = toEpochDay() * 24 * 60 * 60 * 1000

fun Long.toLocalDate(): LocalDate = LocalDate.ofEpochDay(this / (24 * 60 * 60 * 1000))