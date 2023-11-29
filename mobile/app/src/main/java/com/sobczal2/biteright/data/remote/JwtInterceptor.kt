import com.sobczal2.biteright.data.local.UserSPDataSource
import okhttp3.Interceptor
import okhttp3.Response

class JwtInterceptor(private val userSPDataSource: UserSPDataSource) : Interceptor {
    override fun intercept(chain: Interceptor.Chain): Response {
        val original = chain.request()

        val jwt = userSPDataSource.getJwt()
        if (jwt != null) {
            val requestBuilder = original.newBuilder()
                .addHeader("Authorization", "Bearer $jwt")

            val request = requestBuilder.build()
            return chain.proceed(request)
        }

        return chain.proceed(original)
    }
}
